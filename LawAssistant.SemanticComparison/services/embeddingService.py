import chromadb
from chromadb.utils import embedding_functions

CHROMA_FILES_PATH = "./chromaData"

class EmbeddingService:
    def __init__(self):
        self.client = chromadb.PersistentClient(path=CHROMA_FILES_PATH)

        self.embeddingFunc = embedding_functions.SentenceTransformerEmbeddingFunction(
            model_name="intfloat/multilingual-e5-small"
        )

        self.collection = self.client.get_or_create_collection(
            name="embeddings",
            embedding_function=self.embeddingFunc
        )

    def createTextEmbedding(self, id: int, text: str) -> int:
        self.collection.add(
            ids=[str(id)],
            documents=[text],
            metadatas=[{"embeddingId": id}]
        )
        return id

    def compareTextWithEmbedding(self, text: str, embeddingId: int) -> float:
        results = self.collection.query(
            query_texts=[text],
            n_results=1,
            where={"embeddingId": embeddingId}
        )
        distance = results['distances'][0][0]
        similarity = 1 - distance

        return similarity