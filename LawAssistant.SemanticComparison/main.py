from fastapi import FastAPI

from api.router import mainRouter

app = FastAPI()
app.include_router(mainRouter)