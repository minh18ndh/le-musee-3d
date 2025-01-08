import asyncio
import websockets
import json

class HandTrackingServer:
    def __init__(self, host="localhost", port=8765):
        self.host = host
        self.port = port
        self.connected_clients = set()
        
    async def register(self, websocket):
        self.connected_clients.add(websocket)
        try:
            await websocket.wait_closed()
        finally:
            self.connected_clients.remove(websocket)
            
    async def broadcast_state(self, state_data):
        if self.connected_clients:
            message = json.dumps(state_data)
            await asyncio.gather(
                *[client.send(message) for client in self.connected_clients]
            )
            
    async def ws_handler(self, websocket):
        await self.register(websocket)
        
    async def start(self):
        async with websockets.serve(self.ws_handler, self.host, self.port):
            print(f"WebSocket server running on ws://{self.host}:{self.port}")
            await asyncio.Future()  # run forever

server_instance = HandTrackingServer()

async def main():
    server = server_instance
    async with await server.start():
        await asyncio.Future()  # run forever

if __name__ == "__main__":
    asyncio.run(main()) 