from sqlalchemy import create_engine, Column, Integer, String, Text, ForeignKey
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker, Session

POSTGRES_DATABASE_URL = "postgresql://postgres:228@localhost/LawAssistant"

engine = create_engine(POSTGRES_DATABASE_URL)

SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

Base = declarative_base()

def getDatabase():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()