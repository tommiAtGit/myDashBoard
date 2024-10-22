namespace myNotesService.Model{
    public class NotesBase{
        public Guid id;
        public DateTime DateCreatad {get;set;}
        public string ModifiedBy ="";
        public DateTime DateModifyed{get;set;}

        public string Owner="";
        public string NotesTiltle = "";
    }
}