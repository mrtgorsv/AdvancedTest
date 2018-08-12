namespace AdvancedTest.Service.Services.Interface
{
    public interface IDocumentService
    {
        void OpenDocument(int documentId, int userId);
        void ViewDocument(int documentId, int userId);
        int OpenNextDocument(int theoryId, int userId);
    }
}
