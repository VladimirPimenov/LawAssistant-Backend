from sqlalchemy import Column, Integer, String, Date

from database import Base

class Act(Base):
    __tablename__ = "LawAct"

    actId = Column("ActId", Integer, primary_key=True)
    title = Column("Title", String)
    adoptedDate = Column("AdoptedDate", Date)