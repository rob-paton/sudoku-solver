using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    // A Group is a Row, Column, or Box
    internal abstract class Group
    {
        // (Row, Column) of first cell in group, starting from top left of grid
        public (int, int) Location { get; protected set; }
        public Grid Grid { get; set; }
        public Cell[] Cells { get; protected set; }
        // Keeps track of number of notes in Group for each Value
        public Dictionary<int, int> NoteCounts { get; protected set; }

        public Group(Grid grid, (int, int) location) 
        { 
            this.Location = location;
            this.Grid = grid;
            // Initialise each Value with 9 notes (representing notes on 9 Cells)
            this.NoteCounts = new Dictionary<int, int>();
            for (int i = 1; i <= 9; i++)
                this.NoteCounts[i] = 9;
        }

        /// <summary>
        /// Remove given note from other cells in group.
        /// </summary>
        /// <param name="note"></param>
        public void RemoveNotes(int note)
        {
            this.NoteCounts[note] = 0;
            foreach (Cell cell in this.Cells) 
                cell.RemoveNote(note);
        }

        /// <summary>
        /// Decrement NoteCount for given note, and 1 cell left with that note, set PendingValue of cell to note.
        /// </summary>
        /// <param name="note"></param>
        public void DecrementNoteCount(int note)
        {

            if (this.NoteCounts[note] < 2)
                return;

            // If one cell, with this note, left in group after note removal, then find that cell
            if (--this.NoteCounts[note] == 1)
            {
                foreach (Cell cell in this.Cells)
                {
                    // Ignore cells with values
                    if (cell.Value == 0 && cell.Notes.Contains(note))
                    {
                        this.Grid.AddPendingCell(cell, note);
                        break;
                    }
                }
            }
            
        }

        /// <summary>
        /// Check if group contains one of every value
        /// </summary>
        /// <returns>true if group contains one of every value; false if not</returns>
        public bool IsValid()
        {
            LinkedList<int> checklist = new LinkedList<int>();
            for (int i = 1; i <= 9; i++)
                checklist.AddFirst(i);

            // If checklist fails to remove value, it means a previous cell also has that value
            foreach (Cell cell in this.Cells)
                if (!checklist.Remove(cell.Value)) return false;
            return true;
        }
    }
}
