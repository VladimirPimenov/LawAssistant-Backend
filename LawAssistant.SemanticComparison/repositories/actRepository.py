from sqlalchemy.orm import Session

from models.lawAct import Act

class ActRepository:
    def __init__(self, db: Session):
        self.db = db

    def getLawAct(self, id: int) -> Act:
        act = self.db.query(Act).filter(Act.actId == id).first()
        return act