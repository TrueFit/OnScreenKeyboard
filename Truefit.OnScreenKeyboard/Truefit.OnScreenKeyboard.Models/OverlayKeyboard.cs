using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace Truefit.OnScreenKeyboard.Models
{
    public class OverlayKeyboard : INotifyPropertyChanged
    {
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SpaceOccurred;

        private GridPosition? _SelectedGridPosition;
        private GridPosition _currentGridPosition;

        public PathDirection[] GetOptimalPath(GridPosition start, GridPosition finish)
        {
            List<PathDirection> pds = new List<PathDirection>();

            GridPosition currentPos = start;
            int rowDiff = finish.Row - currentPos.Row;
            int colDiff = finish.Column - currentPos.Column;

            // edge cycling enabled (exp: hitting left when at col=0 brings you to rightmost column)

            if(CycleEdges) // path movements can be optimized if cycling is enabled
            {
                // imagine an 8x8 grid where we want to go to row 7 and we're currently at row 2
                // we could go 5 down or 3 up (2->1,1->8,8->7)
                int rowMiddle = (int)Math.Ceiling(PGrid.Rows / (double)2);
                if(rowDiff > rowMiddle) // we need to go down, but we do so by going up and cycling
                {
                    for(int i = 0; i < PGrid.Rows - rowDiff; i++)
                    {
                        pds.Add(PathDirection.Up);
                    }
                    currentPos = new GridPosition(finish.Row, currentPos.Column);
                    rowDiff = 0;
                }
                else if(rowDiff < (rowMiddle*-1)) // we need to go up, but we do so by going down and cycling
                {
                    for(int i = 0; i < PGrid.Rows + rowDiff; i++)
                    {
                        pds.Add(PathDirection.Down);
                    }
                    currentPos = new GridPosition(finish.Row, currentPos.Column);
                    rowDiff = 0;
                }

                int columnMiddle = (int)Math.Ceiling(PGrid.Columns / (double)2);
                if(colDiff > columnMiddle) // we need to go right, but we do so by going left and cycling
                {
                    for(int i = 0; i < PGrid.Columns - colDiff; i++)
                    {
                        pds.Add(PathDirection.Left);
                    }
                    currentPos = new GridPosition(currentPos.Row, finish.Column);
                    colDiff = 0;
                }
                else if(colDiff < (columnMiddle * -1)) // we need to go left, but we do so by going right and cycling
                {
                    for(int i = 0; i < PGrid.Columns + colDiff; i++)
                    {
                        pds.Add(PathDirection.Right);
                    }
                    currentPos = new GridPosition(currentPos.Row, finish.Column);
                    colDiff = 0;
                }
            }

            // finish up after edge cycling, or if edge cycling is disabled, do the entirity of the path

            while(rowDiff>0) // truefit example processes path rows first (i.e. D/U come before L/R)
            {
                GridPosition nextPos = GetNextGridPositionDown(currentPos);
                int downDiff = nextPos.Row - currentPos.Row;
                for(int i=0; i<downDiff; i++)
                {
                    pds.Add(PathDirection.Down);
                }
                currentPos = nextPos;
                rowDiff = finish.Row - currentPos.Row;
            }
            while(rowDiff < 0) 
            {
                GridPosition nextPos = GetNextGridPositionUp(currentPos);
                int upDiff = nextPos.Row - currentPos.Row;
                for(int i = 0; i > upDiff; i--)
                {
                    pds.Add(PathDirection.Up);
                }
                currentPos = nextPos;
                rowDiff = finish.Row - currentPos.Row;
            }
            while(colDiff > 0) // truefit example processes path rows first (i.e. D/U come before L/R)
            {
                GridPosition nextPos = GetNextGridPositionRight(currentPos);
                int rightDiff = nextPos.Column - currentPos.Column;
                for(int i = 0; i < rightDiff; i++)
                {
                    pds.Add(PathDirection.Right);
                }
                currentPos = nextPos;
                colDiff = finish.Column - currentPos.Column;
            }
            while(colDiff < 0)
            {
                GridPosition nextPos = GetNextGridPositionLeft(currentPos);
                int leftDiff = nextPos.Column - currentPos.Column;
                for(int i = 0; i > leftDiff; i--)
                {
                    pds.Add(PathDirection.Left);
                }
                currentPos = nextPos;
                colDiff = finish.Column - currentPos.Column;
            }

            return pds.ToArray();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        protected void OnSpaceOccurred()
        {
            EventHandler handler = SpaceOccurred;
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public OverlayKeyboard(Boolean cycleEdges)
        {
            CycleEdges = cycleEdges;
        }

        public void DoSpace()
        {
            OnSpaceOccurred();
        }

        public void DoSelect()
        {
            SelectedGridPosition = _currentGridPosition;
            string currentVKey = VGrid.VKeys[_currentGridPosition.Row, _currentGridPosition.Column];
            if(!String.IsNullOrEmpty(currentVKey) && currentVKey.Length==1 && currentVKey[0]==' ')
            {
                DoSpace();
            }
        }

        public GridPosition GetNextGridPositionRight(GridPosition currentPos)
        {
            bool foundValidVGrid=false;
            int currentCol;
            for(currentCol=currentPos.Column+1; currentCol<PGrid.Columns; currentCol++)
            {
                if(!String.IsNullOrEmpty(VGrid.VKeys[currentPos.Row, currentCol]))
                {
                    foundValidVGrid = true;
                    break;
                }
            }

            if(foundValidVGrid)
            {
                return new GridPosition(currentPos.Row, currentCol);
            }
            else if(CycleEdges)
            {
                for(currentCol = 0; currentCol < PGrid.Columns; currentCol++)
                {
                    if(!String.IsNullOrEmpty(VGrid.VKeys[currentPos.Row, currentCol]))
                    {
                        return new GridPosition(currentPos.Row, currentCol);
                    }
                }
            }

            return currentPos;
        }

        public GridPosition GetNextGridPositionLeft(GridPosition currentPos)
        {
            bool foundValidVGrid = false;
            int currentCol;
            for(currentCol = currentPos.Column - 1; currentCol >= 0; currentCol--)
            {
                if(!String.IsNullOrEmpty(VGrid.VKeys[currentPos.Row, currentCol]))
                {
                    foundValidVGrid = true;
                    break;
                }
            }

            if(foundValidVGrid)
            {
                return new GridPosition(currentPos.Row, currentCol);
            }
            else if(CycleEdges)
            {
                for(currentCol = PGrid.Columns-1; currentCol >= 0; currentCol--)
                {
                    if(!String.IsNullOrEmpty(VGrid.VKeys[currentPos.Row, currentCol]))
                    {
                        return new GridPosition(currentPos.Row, currentCol);
                    }
                }
            }

            return currentPos;
        }

        public GridPosition GetNextGridPositionUp(GridPosition currentPos)
        {
            bool foundValidVGrid = false;
            int currentRow;
            for(currentRow = currentPos.Row - 1; currentRow >= 0; currentRow--)
            {
                if(!String.IsNullOrEmpty(VGrid.VKeys[currentRow, currentPos.Column]))
                {
                    foundValidVGrid = true;
                    break;
                }
            }

            if(foundValidVGrid)
            {
                return new GridPosition(currentRow, currentPos.Column);
            }
            else if(CycleEdges)
            {
                for(currentRow = PGrid.Rows - 1; currentRow >= 0; currentRow--)
                {
                    if(!String.IsNullOrEmpty(VGrid.VKeys[currentRow, currentPos.Column]))
                    {
                        return new GridPosition(currentRow, currentPos.Column);
                    }
                }
            }

            return currentPos;
        }

        public GridPosition GetNextGridPositionDown(GridPosition currentPos)
        {
            bool foundValidVGrid = false;
            int currentRow;
            for(currentRow = currentPos.Row + 1; currentRow < PGrid.Rows; currentRow++)
            {
                if(!String.IsNullOrEmpty(VGrid.VKeys[currentRow, currentPos.Column]))
                {
                    foundValidVGrid = true;
                    break;
                }
            }

            if(foundValidVGrid)
            {
                return new GridPosition(currentRow, currentPos.Column);
            }
            else if(CycleEdges)
            {
                for(currentRow = 0; currentRow < PGrid.Rows; currentRow++)
                {
                    if(!String.IsNullOrEmpty(VGrid.VKeys[currentRow, currentPos.Column]))
                    {
                        return new GridPosition(currentRow, currentPos.Column);
                    }
                }
            }

            return currentPos;
        }

        public void Load(string pathToXml)
        {
            XElement root = XElement.Load(pathToXml);
            PGrid =
                (from pGridElement in root.Elements("PGrid")
                select new OKPGrid(Convert.ToInt32(pGridElement.Attribute("rows").Value), 
                                   Convert.ToInt32(pGridElement.Attribute("columns").Value))).First();
            var vkeyquery =
                from vKeyElement in root.Element("VGrid").Elements("VKey")
                select new
                {
                    Row = Convert.ToInt32(vKeyElement.Attribute("row").Value),
                    Column = Convert.ToInt32(vKeyElement.Attribute("column").Value),
                    Value = vKeyElement.Value
                };

            string[,] vkeys = new string[PGrid.Rows, PGrid.Columns];
            foreach(var vkey in vkeyquery) { vkeys[vkey.Row-1, vkey.Column-1] = vkey.Value; }
            VGrid = new OKVGrid() { VKeys = vkeys };
        }

        public OKPGrid PGrid { get; private set; }
        public OKVGrid VGrid { get; private set; }
        public Boolean CycleEdges { get; set; }
 
        public GridPosition CurrentGridPosition
        {
            get
            {
                return _currentGridPosition;
            }
            set
            {
                _currentGridPosition = value;
                OnPropertyChanged("CurrentGridPosition");
            }
        }

        public GridPosition? SelectedGridPosition
        {
            get
            {
                return _SelectedGridPosition;
            }
            private set
            {
                _SelectedGridPosition = value;
                OnPropertyChanged("SelectedGridPosition");
            }
        }
    }
}
