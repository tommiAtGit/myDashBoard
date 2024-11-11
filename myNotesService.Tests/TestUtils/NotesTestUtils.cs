using AutoMapper;
using myNotesService.Domain;
using myNotesService.Model;

namespace myNotesService.TestUtils
{
    public class NotesTestUtils
    {
        public static int NUMBER_OF_NOTES = 8;

        public int GetNumberOfNotes()
        {
            return NUMBER_OF_NOTES;
        }
        public static GeneralNotes CreateGeneralNotes()
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
        public static GeneralNotesDTO CreateGeneralNotesDTO(IMapper mapper){
            return mapper.Map<GeneralNotesDTO>(CreateGeneralNotes());
        }

        public static List<GeneralNotes> CreateMultipleGeneralNotes()
        {
            List<GeneralNotes> testNotes = [];

            for (int i = 0; i < NUMBER_OF_NOTES; i++)
            {
                GeneralNotes notes = new()
                {
                    id = Guid.NewGuid(),
                    DateCreatad = DateTime.Now.AddDays(-2 - i),
                    ModifiedBy = "Tommi_#" + i,
                    DateModifyed = DateTime.Now.AddDays(i*(-1)),

                    Owner = "Owner Tommi_#" + i,
                    NotesTiltle = "Document test title_#" + i,
                    Notes = "Test Notes content_#" + i,
                    NotesConclution = "Test Notes conclution_#" + i,

                    KeyWords = ["Tommi_#" + i+".1", "Test_#" + i+".2", "Notes_#" + i+".3"]
                };
                testNotes.Add(notes);
            }
            return testNotes;
        }
        public static List<GeneralNotesDTO>CreateMultibleGeneralNotesDTO(IMapper mapper){
            return mapper.Map<List<GeneralNotesDTO>> (CreateMultipleGeneralNotes());
        }

    }
}

