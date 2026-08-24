from tomllib import load
from pathlib import Path
from pydantic import BaseModel

class ServerConfig(BaseModel):
    host: str
    port: int
    docsEndpoint: str

class DatabaseConfig(BaseModel):
    connectionString : str

class Config(BaseModel):
    server: ServerConfig
    database: DatabaseConfig

appPath = Path(__file__).parent
configPath = appPath / "config.toml"

with open(configPath, "rb") as configFile:
    config = Config.model_validate(load(configFile))