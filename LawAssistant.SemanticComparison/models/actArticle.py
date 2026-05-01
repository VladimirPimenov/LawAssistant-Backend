from sqlalchemy import Column, Integer, String, Text

from database import Base

class ActArticle(Base):
    __tablename__ = "ActArticle"

    articleId = Column("ArticleId", Integer, primary_key=True)
    number = Column("Number", String)
    actId = Column("ActId",Integer)
    title = Column("Title", String)
    text = Column("Text", Text)