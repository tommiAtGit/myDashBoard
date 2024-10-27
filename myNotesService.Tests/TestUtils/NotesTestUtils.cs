using myNotesService.Model;

namespace myNotesService.TestUtils
{
    public class NotesTestUtils
    {
        public static int NUMBER_OF_NOTES = 8;

        public int GetNumberOfNotes(){
            return NUMBER_OF_NOTES;
        }
        public GeneralNotes CreateGeneralNotes()
        {

            return new GeneralNotes()
            {
                id = Guid.NewGuid(),
                DateCreatad = DateTime.Now.AddDays(-2),
                ModifiedBy = "Tommi",
                DateModifyed = DateTime.Now,

                Owner = "Owner Tommi",
                NotesTiltle = "Document test title",
                Notes = "Test Notes content",
                NotesConclution = "Test Notes conclution",

                KeyWords = ["Tommi", "Test", "Notes"]


            };
        }
        public IEnumerable<GeneralNotes> CreateMultipleGeneralNotes()
        {
            IEnumerable<GeneralNotes> testNotes = [];

            for (int i = 0; i < NUMBER_OF_NOTES; i++)
            {
                GeneralNotes notes = new()
                {
                    id = Guid.NewGuid(),
                    DateCreatad = DateTime.Now.AddDays(-2-i),
                    ModifiedBy = "Tommi_#"+i,
                    DateModifyed = DateTime.Now,

                    Owner = "Owner Tommi_#"+i,
                    NotesTiltle = "Document test title_#"+i,
                    Notes = "Test Notes content_#"+i,
                    NotesConclution = "Test Notes conclution_#"+i,

                    KeyWords = ["Tommi_#"+i, "Test_#"+i, "Notes_#"+i]
                };
                testNotes.Append(notes);


            }
            return testNotes;
        }
    }
}

