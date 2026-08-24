import uvicorn

from fastapi import FastAPI

from api.router import mainRouter
from config import config

app = FastAPI(docs_url=config.server.docsEndpoint)
app.include_router(mainRouter)

if __name__ == "__main__":
    uvicorn.run(
        "main:app",
        host=config.server.host,
        port=config.server.port,
        reload=True,
    )