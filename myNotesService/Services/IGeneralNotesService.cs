using myNotesService.Model;

namespace myNotesService.Services{
    public interface IGeneralNotesService{
        public IEnumerable<GeneralNotes> GetAllGeneralNotes();
        public GeneralNotes GetGeneralNotesById(Guid id);

        public GeneralNotes GetGeneralNotesByOwner(string owner);

        public GeneralNotes GetGeneralNotesByDateCreated(DateTime dateCreated);

        public IEnumerable <GeneralNotes> GetGeneralNotesByKeyWords(List<string> keyWords);

        public GeneralNotes AddGeneralNotes(GeneralNotes notes);

        public GeneralNotes UpdateGeneralNotes(GeneralNotes notes);

        public bool DeleteGeneralNote(Guid id);
    }
}