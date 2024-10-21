using myNotesService.Model;
using myNotesService.Repository;

namespace myNotesService.Services{
    public class GeneralNotesService : IGeneralNotesService
    {
        public GeneralNotes AddGeneralNotes(GeneralNotes notes)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGeneralNote(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GeneralNotes> GetAllGeneralNotes()
        {
            throw new NotImplementedException();
        }

        public GeneralNotes GetGeneralNotesByDateCreated(DateTime dateCreated)
        {
            throw new NotImplementedException();
        }

        public GeneralNotes GetGeneralNotesById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GeneralNotes> GetGeneralNotesByKeyWords(List<string> keyWords)
        {
            throw new NotImplementedException();
        }

        public GeneralNotes GetGeneralNotesByOwner(string owner)
        {
            throw new NotImplementedException();
        }

        public GeneralNotes UpdateGeneralNotes(GeneralNotes notes)
        {
            throw new NotImplementedException();
        }
    }

}