import cv2
import mediapipe as mp
import time
from enum import Enum
import asyncio
import json
from websocket_server import server_instance
import threading
import numpy as np

class HandState(Enum):
    NO_HAND = "Khong co tay"
    LEFT_HAND = "Tay trai"
    RIGHT_HAND = "Tay phai"
    BOTH_HANDS = "Hai tay"
    
    def to_dict(self):
        return {
            "state": self.name,
            "value": self.value,
            "timestamp": time.time()
        }

class HandDetector:
    def __init__(self, mode=False, max_hands=2, detection_confidence=0.5, tracking_confidence=0.5):
        self.mode = mode
        self.max_hands = max_hands
        self.detection_confidence = detection_confidence
        self.tracking_confidence = tracking_confidence

        self.mp_hands = mp.solutions.hands
        self.hands = self.mp_hands.Hands(
            static_image_mode=self.mode,
            max_num_hands=self.max_hands,
            min_detection_confidence=self.detection_confidence,
            min_tracking_confidence=self.tracking_confidence
        )
        self.mp_draw = mp.solutions.drawing_utils
        
        self.current_state = HandState.NO_HAND
        self.state_start_time = time.time()
        self.state_duration = 0
        
        self.loop = None
        self.start_websocket_server()

    def start_websocket_server(self):
        def run_server():
            self.loop = asyncio.new_event_loop()
            asyncio.set_event_loop(self.loop)
            self.loop.run_until_complete(server_instance.start())
            self.loop.run_forever()

        self.server_thread = threading.Thread(target=run_server, daemon=True)
        self.server_thread.start()
        time.sleep(1)

    def broadcast_state(self, state):
        if self.loop is not None:
            state_data = state.to_dict()
            asyncio.run_coroutine_threadsafe(
                server_instance.broadcast_state(state_data), 
                self.loop
            )

    def check_fingers_up(self, hand_landmarks, handedness):
        # Các điểm mốc cho các ngón tay
        finger_tips = [4, 8, 12, 16, 20]  # Đầu ngón
        finger_pips = [3, 7, 11, 15, 19]  # Khớp giữa
        
        fingers = []
        
        # Kiểm tra các ngón (bao gồm cả ngón cái)
        for id in range(5):
            finger_tip = hand_landmarks.landmark[finger_tips[id]]
            finger_pip = hand_landmarks.landmark[finger_pips[id]]
            
            # Ngón xòe ra khi đầu ngón cao hơn khớp giữa (y nhỏ hơn)
            fingers.append(finger_tip.y < finger_pip.y)
        
        # Trả về True nếu tất cả ngón đều xòe ra
        return all(fingers)

    def find_hands(self, img, draw=True):
        img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
        self.results = self.hands.process(img_rgb)
        
        if self.results.multi_hand_landmarks:
            for hand_landmarks in self.results.multi_hand_landmarks:
                if draw:
                    self.mp_draw.draw_landmarks(
                        img, 
                        hand_landmarks, 
                        self.mp_hands.HAND_CONNECTIONS
                    )
        return img

    def get_hand_status(self):
        left_hand = False
        right_hand = False
        
        if self.results.multi_handedness:
            for idx, (hand_landmarks, handedness) in enumerate(
                zip(self.results.multi_hand_landmarks, self.results.multi_handedness)
            ):
                # Chỉ tính là có tay khi tất cả ngón đều xòe ra
                if self.check_fingers_up(hand_landmarks, handedness):
                    # Đảo ngược Left/Right vì MediaPipe trả về ngược với góc nhìn người dùng
                    if handedness.classification[0].label == "Left":
                        right_hand = True  # Tay phải của người dùng
                    elif handedness.classification[0].label == "Right":
                        left_hand = True   # Tay trái của người dùng
        
        new_state = HandState.NO_HAND
        if left_hand and right_hand:
            new_state = HandState.BOTH_HANDS
        elif left_hand:
            new_state = HandState.LEFT_HAND
        elif right_hand:
            new_state = HandState.RIGHT_HAND
            
        if new_state != self.current_state:
            self.current_state = new_state
            self.state_start_time = time.time()
            self.broadcast_state(new_state)
        
        self.state_duration = time.time() - self.state_start_time
        return self.current_state

def main():
    cap = cv2.VideoCapture(0)
    detector = HandDetector()
    
    state_colors = {
        HandState.NO_HAND: (0, 0, 255),
        HandState.LEFT_HAND: (0, 255, 0),
        HandState.RIGHT_HAND: (255, 0, 0),
        HandState.BOTH_HANDS: (255, 255, 0)
    }
    
    while True:
        success, img = cap.read()
        if not success:
            break
            
        img = detector.find_hands(img)
        current_state = detector.get_hand_status()
        
        color = state_colors[current_state]
        cv2.putText(img, f"Trang thai: {current_state.value}", (10, 30), 
                    cv2.FONT_HERSHEY_SIMPLEX, 0.8, color, 2)
        cv2.putText(img, f"Thoi gian: {detector.state_duration:.1f}s", (10, 60),
                    cv2.FONT_HERSHEY_SIMPLEX, 0.8, color, 2)
        
        cv2.rectangle(img, (0, 0), (img.shape[1], img.shape[0]), color, 2)
        
        cv2.imshow("Hand Tracking", img)
        
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break
            
    cap.release()
    cv2.destroyAllWindows()

if __name__ == "__main__":
    main()
