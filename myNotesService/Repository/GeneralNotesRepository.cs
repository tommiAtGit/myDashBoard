using myNotesService.Domain;
using myNotesService.Repository;

namespace myNotesService.Repository
{

public class GeneralNotesReporitory : IGeneralNotesRpository
{
    private List<GeneralNotesDTO> _genNotes;

    public GeneralNotesReporitory()
    {
        _genNotes = [];
        GenerateMockGeneralNotes();
    }
    public GeneralNotesDTO AddGeneralNotes(GeneralNotesDTO notes)
    {
        if (notes != null)
        {
            notes.id = Guid.NewGuid();
            _genNotes.Add(notes);
            return notes;
        }
        else
        {
            throw new ArgumentNullException();
        }


    }

    public bool DeleteGeneralNote(Guid id)
    {
        GeneralNotesDTO theNotes = GetGeneralNotesById(id);
        if (theNotes.id != Guid.Empty)
            return _genNotes.Remove(theNotes);
        else
            return false;


    }

    public IEnumerable<GeneralNotesDTO> GetAllGeneralNotes()
    {
        return _genNotes;
    }

    public IEnumerable<GeneralNotesDTO> GetGeneralNotesByDateCreated(DateTime dateCreated)
    {
        var notes = _genNotes.FindAll(p => p.DateCreatad.Date == dateCreated.Date);
        
        if (notes != null)
            return notes;
        else
            throw new ArgumentNullException("NotFound", nameof(notes));

    }

    public GeneralNotesDTO GetGeneralNotesById(Guid id)
    {
        var notes = _genNotes.FirstOrDefault(p => p.id == id);
        if (notes != null)
            return notes;
        else
            return new GeneralNotesDTO();

    }

    public IEnumerable<GeneralNotesDTO> GetGeneralNotesByKeyWords(List<string> keyWords)
    {
        List<GeneralNotesDTO> keyWordNotes = [];
        var notes = GetAllGeneralNotes();
        foreach (GeneralNotesDTO o in notes)
        {
            foreach (string keyWord in keyWords)
            {
                if (o.KeyWords.Contains(keyWord))
                {
                    keyWordNotes.Add(o);
                }
            }
        }
        return keyWordNotes;
    }

    public IEnumerable<GeneralNotesDTO> GetGeneralNotesByOwner(string owner)
    {
        var notes = _genNotes.FindAll(p => p.Owner.Equals(owner));
        if (notes != null)
            return notes;
        else
            throw new ArgumentNullException("NotFound", nameof(owner));

    }

    public GeneralNotesDTO UpdateGeneralNotes(Guid id,GeneralNotesDTO notes)
    {
        var n = GetGeneralNotesById(id);
        if(n.id==Guid.Empty)
            throw new ArgumentNullException("NotFound", nameof(notes));
        int index = _genNotes.IndexOf(n);
        _genNotes[index] = notes;
        return notes;
    }

    private void GenerateMockGeneralNotes(){
        _genNotes.Add(new GeneralNotesDTO {
            id = Guid.NewGuid(),
            NotesTiltle = "First Note",
            Notes = "This is the content of the first note. It covers various topics related to project management and design.",
            NotesConclution = "Conclusion of the first note: prioritize tasks.",
            KeyWords = ["test", "first", "management"],
            DateCreatad = DateTime.Now,
            Owner = "Tommi"
        });
        _genNotes.Add(new GeneralNotesDTO {
            id = Guid.NewGuid(),
            NotesTiltle = "Second Note",
            Notes = "This is the content of the second note. Focuses on development workflows and backend services.",
            NotesConclution = "Conclusion of the second note: use automation.",
            KeyWords = ["test", "second", "development"],
            DateCreatad = DateTime.Now.AddDays(-1),
            Owner = "Tommi"
        });

    }

}
}