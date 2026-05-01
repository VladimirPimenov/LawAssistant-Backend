from sqlalchemy.orm import Session
from typing import List

from models.lawAct import Act
from models.actArticle import ActArticle

from repositories.actRepository import ActRepository
from repositories.articleRepository import ArticleRepository

from services.embeddingService import EmbeddingService

class ActService:
    def __init__(self, db: Session):
        self.actRepository = ActRepository(db)
        self.articleRepository = ArticleRepository(db)
        self.embeddingService = EmbeddingService()

    def createActEmbeddings(self, id: int) -> Act:
        act = self.actRepository.getLawAct(id)
        if not act:
            return None

        articles = self.articleRepository.getActArticles(act.actId)
        currentCount = 0
        count = len(articles)

        for article in articles:
            self.embeddingService.createTextEmbedding(article.articleId, article.text)
            currentCount += 1
            print(f"{currentCount}/{count}")

        return act