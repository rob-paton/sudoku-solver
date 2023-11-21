using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class Cell
    {
        // (Row, Column) starting from top left of grid
        public (int, int) Location { get; private set; }
        public int Value { get; set; }
        // Once cell's value is determined, it is stored here and set to value once current method is done
        public int PendingValue { get; set; }
        public int NotesCount { get; private set; }
        public LinkedList<int> Notes { get; private set; }
        public Grid Grid { get; set; }
        public Row Row { get; set; }
        public Column Column { get; set; }
        public Box Box { get; set; }

        public Cell(Grid grid, (int, int) location)
        {
            this.Location = location;
            this.Value = 0;
            this.PendingValue = 0;
            this.NotesCount = 9;
            this.Notes = new LinkedList<int>();
            for (int i = 1; i <= 9; i++)
                this.Notes.AddFirst(i);
            this.Grid = grid;
        }

        /// <summary>
        /// Removes note from Cell.Notes
        /// </summary>
        /// <param name="note"></param>
        public void RemoveNote(int note)
        {
            // Ignore cells that have no notes (and therefore have a value)
            if (this.Value == 0)
            {
                if (this.Notes.Remove(note))
                {
                    this.NotesCount--;
                    // If one note left in cell after note removal, then give cell a PendingValue of the remaining note
                    if (this.NotesCount == 1)
                        this.Grid.AddPendingCell(this, this.Notes.First());
                    
                    this.DecrementGroupNoteCounts(note);
                }
            }
        }

        /// <summary>
        /// Set value of cell to given value, and remove notes of this value from other cells in this cell's groups
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            this.Value = value;
            this.NotesCount = 0;

            // Remove notes for this value in cell's groups
            this.Row.RemoveNotes(value);
            this.Column.RemoveNotes(value);
            this.Box.RemoveNotes(value);

            // Decrement group notecounts for notes still on this cell
            while (this.Notes.Count > 0) 
            {
                int note = this.Notes.First();
                this.Notes.RemoveFirst();
                this.DecrementGroupNoteCounts(note);
            }
        }

        private void DecrementGroupNoteCounts(int note)
        {
            this.Row.DecrementNoteCount(note);
            this.Column.DecrementNoteCount(note);
            this.Box.DecrementNoteCount(note);
        }
    }
}
