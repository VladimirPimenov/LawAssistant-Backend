from sqlalchemy.orm import Session
from typing import List

from repositories.articleRepository import ArticleRepository
from models.actArticle import ActArticle

class ArticleService:
    def __init__(self, db: Session):
        self.articleRepository = ArticleRepository(db)

    def getArticleById(self, id: int) -> ActArticle:
        article = self.articleRepository.getArticle(id)

        if article:
            return article
        return None

    def getActArticles(self, actId: int) -> List[ActArticle]:
        articles = self.articleRepository.getActArticles(actId)
        return articles