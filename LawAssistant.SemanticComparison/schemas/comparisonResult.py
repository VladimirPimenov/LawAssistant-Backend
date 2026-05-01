from pydantic import BaseModel

class ArticleSchema(BaseModel):
    articleId: int
    actId: int
    number: str
    title: str
    text: str

class ComparisonResultSchema(BaseModel):
    resultId: int
    article: ArticleSchema
    text: str
    matchValue: float