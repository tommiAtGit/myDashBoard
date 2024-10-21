namespace myNotesService.Domain{
    public class GeneralNotesDTO:NotesDTOBase{
        public string Notes {get;set;} ="";
        public string NotesConclution {get;set;} = "";

        public List<string> KeyWords {get;set;} = [];
    }
}