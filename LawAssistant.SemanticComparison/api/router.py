from fastapi import APIRouter

from api.comparisonResult import router as comparisonRouter
from api.act import router as actRouter

mainRouter = APIRouter()

mainRouter.include_router(comparisonRouter)
mainRouter.include_router(actRouter)