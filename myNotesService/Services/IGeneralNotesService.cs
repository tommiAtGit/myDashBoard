using myNotesService.Model;

namespace myNotesService.Services{
    public interface IGeneralNotesService{
        public IEnumerable<GeneralNotes> GetAllGeneralNotes();
        public GeneralNotes GetGeneralNotesById(Guid id);

        public IEnumerable <GeneralNotes> GetGeneralNotesByOwner(string owner);

        public IEnumerable<GeneralNotes> GetGeneralNotesByDateCreated(DateTime dateCreated);

        public IEnumerable <GeneralNotes> GetGeneralNotesByKeyWords(List<string> keyWords);

        public GeneralNotes AddGeneralNotes(GeneralNotes notes);

        public GeneralNotes UpdateGeneralNotes(Guid id, GeneralNotes notes);

        public bool DeleteGeneralNote(Guid id);
    }
}