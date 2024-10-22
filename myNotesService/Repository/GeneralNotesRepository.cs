using myNotesService.Domain;
using myNotesService.Repository;

namespace myNotesService.Repository{

}
public class GeneralNotesReporitory : IGeneralNotesRpository
{
    private List<GeneralNotesDTO> genNotes;

    public GeneralNotesReporitory(){
        genNotes = [];
    }
    public GeneralNotesDTO AddGeneralNotes(GeneralNotesDTO notes)
    {
        notes.id = Guid.NewGuid();
        genNotes.Add(notes);
        return notes;

    }

    public bool DeleteGeneralNote(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GeneralNotesDTO> GetAllGeneralNotes()
    {
        throw new NotImplementedException();
    }

    public GeneralNotesDTO GetGeneralNotesByDateCreated(DateTime dateCreated)
    {
        throw new NotImplementedException();
    }

    public GeneralNotesDTO GetGeneralNotesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GeneralNotesDTO> GetGeneralNotesByKeyWords(List<string> keyWords)
    {
        throw new NotImplementedException();
    }

    public GeneralNotesDTO GetGeneralNotesByOwner(string owner)
    {
        throw new NotImplementedException();
    }

    public GeneralNotesDTO UpdateGeneralNotes(GeneralNotesDTO notes)
    {
        throw new NotImplementedException();
    }
}