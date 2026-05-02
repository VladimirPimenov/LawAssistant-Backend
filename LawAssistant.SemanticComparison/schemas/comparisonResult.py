from pydantic import BaseModel

class ParagraphSchema(BaseModel):
    paragraphId: int
    contractId: int
    text: str

class ComparisonResultSchema(BaseModel):
    resultId: int
    articleId: int
    paragraphId: int
    contractParagraph: ParagraphSchema
    text: str
    matchValue: float