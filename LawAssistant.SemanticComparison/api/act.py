from fastapi import APIRouter, HTTPException, Depends
from sqlalchemy.orm import Session

from services.actService import ActService
from schemas.act import ActEmbeddingRequest
from database import getDatabase

router = APIRouter(prefix="/acts")

def getActService(db: Session = Depends(getDatabase)):
    return ActService(db)

@router.post("/create-act-embedding")
def createActEmbedding(
    actRequest: ActEmbeddingRequest,
    actService: ActService = Depends(getActService)
):
    embeddedAct = actService.createActEmbeddings(actRequest.actId)
    
    if not embeddedAct:
        raise HTTPException(status_code=404, detail="Act not found or embedding failed")
    return embeddedAct