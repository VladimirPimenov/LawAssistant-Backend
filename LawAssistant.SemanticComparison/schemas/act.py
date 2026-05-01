from pydantic import BaseModel
from datetime import date

class ActEmbeddingRequest(BaseModel):
    actId: int
    title: str
    adoptedDate: date