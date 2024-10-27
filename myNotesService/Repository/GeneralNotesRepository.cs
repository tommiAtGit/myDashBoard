using myNotesService.Domain;
using myNotesService.Repository;

namespace myNotesService.Repository
{

}
public class GeneralNotesReporitory : IGeneralNotesRpository
{
    private List<GeneralNotesDTO> _genNotes;

    public GeneralNotesReporitory()
    {
        _genNotes = [];
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
        if (theNotes != null)
            return _genNotes.Remove(theNotes);
        else
            throw new ArgumentNullException("NotFound", nameof(theNotes));


    }

    public IEnumerable<GeneralNotesDTO> GetAllGeneralNotes()
    {
        return _genNotes;
    }

    public IEnumerable<GeneralNotesDTO> GetGeneralNotesByDateCreated(DateTime dateCreated)
    {
        var notes = _genNotes.FindAll(p => p.DateCreatad == dateCreated);
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
            throw new ArgumentNullException("NotFound", nameof(notes));

    }

    public IEnumerable<GeneralNotesDTO> GetGeneralNotesByKeyWords(List<string> keyWords)
    {
        IEnumerable<GeneralNotesDTO> keyWordNotes = [];
        var notes = GetAllGeneralNotes();
        foreach (var o in notes)
        {
            foreach (string keyWord in o.KeyWords)
            {
                if (o.KeyWords.Contains(keyWord))
                {
                    keyWordNotes.Append(o);
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
        if(n==null)
            throw new ArgumentNullException("NotFound", nameof(notes));
        int index = _genNotes.IndexOf(n);
        _genNotes.Remove(n);
        _genNotes.Add(notes);
        return notes;

        

    }

}