from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

from schemas.comparisonResult import ComparisonResultSchema
from services.compareService import CompareService

router = APIRouter(prefix="/compare")

def getCompareService():
    return CompareService()

@router.put("/compare-with-embedding")
def compareWithEmbedding(
    compareResult: ComparisonResultSchema,
    compareService: CompareService = Depends(getCompareService)
):
    updatedResult = compareService.calculateSemanticSimilarity(compareResult)
    return updatedResult