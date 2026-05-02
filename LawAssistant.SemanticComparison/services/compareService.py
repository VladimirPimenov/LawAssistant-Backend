from schemas.comparisonResult import ComparisonResultSchema

from services.embeddingService import EmbeddingService

class CompareService:
    def __init__(self):
        self.embeddingService = EmbeddingService()

    def calculateSemanticSimilarity(self, compareResult: ComparisonResultSchema) -> ComparisonResultSchema:
        paragraphText = compareResult.contractParagraph.text
        articleId = compareResult.articleId

        similarity = self.embeddingService.compareTextWithEmbedding(paragraphText, articleId)
        compareResult.matchValue = similarity

        return compareResult