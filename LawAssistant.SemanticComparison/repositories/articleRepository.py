from sqlalchemy.orm import Session
from typing import List

from models.actArticle import ActArticle

class ArticleRepository:
    def __init__(self, db: Session):
        self.db = db

    def getArticle(self, id: int) -> ActArticle:
        article = self.db.query(ActArticle).filter(ActArticle.articleId == id).first()
        return article

    def getActArticles(self, actId: int) -> List[ActArticle]:
        articles = self.db.query(ActArticle).filter(ActArticle.actId == actId).all()
        return articles