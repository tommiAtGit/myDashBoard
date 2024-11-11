namespace myNotesService.Model{
    
    public class GeneralNotes:NotesBase{
        public string Notes {get;set;} ="";
        public string NotesConclution {get;set;} = "";

        public List<string> KeyWords {get;set;} = [];

    }
    
}