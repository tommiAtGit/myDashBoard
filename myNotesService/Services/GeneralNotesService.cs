using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using myNotesService.Domain;
using myNotesService.Model;
using myNotesService.Repository;

namespace myNotesService.Services
{
    public class GeneralNotesService : IGeneralNotesService
    {
        private readonly IMapper _mapper;
        private readonly IGeneralNotesRpository _repository;

        public GeneralNotesService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new GeneralNotesReporitory();

        }
        public GeneralNotes AddGeneralNotes(GeneralNotes notes)
        {
            if (notes == null)
                throw new ArgumentNullException(nameof(notes), "No task defined");

            var notesDTO = _repository.AddGeneralNotes(_mapper.Map<GeneralNotesDTO>(notes));

            return _mapper.Map<GeneralNotes>(notesDTO);

        }

        public bool DeleteGeneralNote(Guid id)
        {
            GeneralNotesDTO genNotes = _repository.GetGeneralNotesById(id);
            if (genNotes != null)
            {
                var result = _repository.DeleteGeneralNote(genNotes.id);
                return result;
            }
            else
                throw new ArgumentNullException(nameof(genNotes), "No task defined");
        }


        public IEnumerable<GeneralNotes> GetAllGeneralNotes()
        {
            return _mapper.Map<IEnumerable<GeneralNotes>>(_repository.GetAllGeneralNotes());
        }

        public IEnumerable<GeneralNotes> GetGeneralNotesByDateCreated(DateTime dateCreated)
        {
            return _mapper.Map<IEnumerable<GeneralNotes>>(_repository.GetGeneralNotesByDateCreated(dateCreated));

        }

        public GeneralNotes GetGeneralNotesById(Guid id)
        {
            if (id == Guid.Empty)
                return new GeneralNotes();
            return _mapper.Map<GeneralNotes>(_repository.GetGeneralNotesById(id));
        }

        public IEnumerable<GeneralNotes> GetGeneralNotesByKeyWords(List<string> keyWords)
        {
            if (keyWords.Count < 1)
                return[];

            return _mapper.Map<IEnumerable<GeneralNotes>>(_repository.GetGeneralNotesByKeyWords(keyWords));
        }

        public IEnumerable<GeneralNotes> GetGeneralNotesByOwner(string owner)
        {
            if ((owner == null) || owner == "")
                return [];
            return _mapper.Map<IEnumerable<GeneralNotes>>(_repository.GetGeneralNotesByOwner(owner));

        }

        public GeneralNotes UpdateGeneralNotes(Guid id, GeneralNotes notes)
        {
            var genNotes = _repository.GetGeneralNotesById(id);
            if(genNotes.id==Guid.Empty)
                return new GeneralNotes();
            genNotes.DateCreatad = notes.DateCreatad;
            genNotes.DateModifyed = notes.DateModifyed;
            genNotes.KeyWords = notes.KeyWords;
            genNotes.ModifiedBy = notes.ModifiedBy;
            genNotes.Notes = notes.Notes;
            genNotes.NotesConclution = notes.NotesConclution;
            genNotes.NotesTiltle = notes.NotesTiltle;
            genNotes.Owner = notes.Owner;
            return _mapper.Map<GeneralNotes>(_repository.UpdateGeneralNotes(id, genNotes));
        }
    }

}