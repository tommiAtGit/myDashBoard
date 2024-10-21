using myNotesService.Domain;

namespace myNotesService.Repository{
    public interface IGeneralNotesRpository{
        public IEnumerable<GeneralNotesDTO> GetAllGeneralNotes();
        public GeneralNotesDTO GetGeneralNotesById(Guid id);

        public GeneralNotesDTO GetGeneralNotesByOwner(string owner);

        public GeneralNotesDTO GetGeneralNotesByDateCreated(DateTime dateCreated);

        public IEnumerable <GeneralNotesDTO> GetGeneralNotesByKeyWords(List<string> keyWords);

        public GeneralNotesDTO AddGeneralNotes(GeneralNotesDTO notes);

        public GeneralNotesDTO UpdateGeneralNotes(GeneralNotesDTO notes);

        public bool DeleteGeneralNote(Guid id);
    }
}