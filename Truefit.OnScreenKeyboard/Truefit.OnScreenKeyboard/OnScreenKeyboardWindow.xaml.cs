using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;

using Truefit.OnScreenKeyboard.Models;
using Truefit.OnScreenKeyboard.Controls;

namespace Truefit.OnScreenKeyboard
{
    public partial class OnScreenKeyboardWindow : Window
    {
        private OverlayKeyboard _kb;
        private ArrowPadRemoteControlWithVTT _remoteControl;
        private GridPosition _lastSelectedGridPosition;

        public OnScreenKeyboardWindow()
        {
            InitializeComponent();

            FileNameOverlayKeyboardDefinition.Text = Properties.Settings.Default.OverlayKeyboardDefinitionPath;
            FileNameVoiceCommandsFlatFile.Text = Properties.Settings.Default.VoiceCommandsFlatFilePath;

            _kb = new OverlayKeyboard(false);
            _kb.PropertyChanged += OnKeyboardPropertyChanged;
        }

        private void OnKeyboardSpaceOccurred(object sender, EventArgs e)
        {          
            // as our keyboards do not have an explicit spacebar, we get their txt
            // and path output in this manner
            TextOutput.AppendText(" ");
            PathOutput.AppendText("S,");
        }

        private void OnKeyboardPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "CurrentGridPosition":
                    DrawKeyboard(_kb);
                    break;
                case "SelectedGridPosition":
                    DrawKeyboard(_kb); // redraw the keyboard to update highlighting
                    if(_kb.SelectedGridPosition.HasValue)
                    {
                        PathDirection[] directions = _kb.GetOptimalPath(_lastSelectedGridPosition, _kb.SelectedGridPosition.Value);

                        if(directions.Length > 0)
                        {
                            foreach(PathDirection direction in directions)
                            {
                                // U = up,  D = down, L = left, R = right, S = space (handled elsewhere), # = select
                                switch(direction)
                                {
                                    case PathDirection.Up:
                                        PathOutput.AppendText("U,");
                                        break;
                                    case PathDirection.Down:
                                        PathOutput.AppendText("D,");
                                        break;
                                    case PathDirection.Left:
                                        PathOutput.AppendText("L,");
                                        break;
                                    case PathDirection.Right:
                                        PathOutput.AppendText("R,");
                                        break;
                                }
                            }
                            PathOutput.AppendText("#,");
                        }
                        TextOutput.AppendText(_kb.VGrid.VKeys[_kb.SelectedGridPosition.Value.Row, _kb.SelectedGridPosition.Value.Column]);
                        _lastSelectedGridPosition = _kb.SelectedGridPosition.Value;
                    }
                    break;
            }
        }

        private void BrowseOverlayKeyboardXMLDefinition_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileNameOverlayKeyboardDefinition.Text = ofd.FileName;
            }
        }

        private void LoadOverlayKeyboardXMLDefinition_Click(object sender, RoutedEventArgs e)
        {
            if(!File.Exists(FileNameOverlayKeyboardDefinition.Text)) return;

            _kb.Load(FileNameOverlayKeyboardDefinition.Text);
            _kb.CycleEdges = CycleEdges.IsChecked.Value;
            _remoteControl = new ArrowPadRemoteControlWithVTT(_kb);
            DrawKeyboard(_kb);
            Properties.Settings.Default.OverlayKeyboardDefinitionPath = FileNameOverlayKeyboardDefinition.Text;
            Properties.Settings.Default.Save();            
        }

        private void BrowseVoiceCommandsFlatFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ofd.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileNameVoiceCommandsFlatFile.Text = ofd.FileName;
            }
        }

        private void LoadVoiceCommandsFlatFile_Click(object sender, RoutedEventArgs e)
        {
            if(!File.Exists(FileNameVoiceCommandsFlatFile.Text)) return;
            string[] allLines = File.ReadAllLines(FileNameVoiceCommandsFlatFile.Text);

            if(allLines.Length==0)
            {
                System.Windows.MessageBox.Show("File contained no commands (one command per line)");
                return;
            }

            VoiceCommandsComboBox.Items.Clear();
            foreach(string line in allLines)
            {
                VoiceCommandsComboBox.Items.Add(line);
            }
            VoiceCommandsComboBox.SelectedIndex = 0;
            
            Properties.Settings.Default.VoiceCommandsFlatFilePath = FileNameVoiceCommandsFlatFile.Text;
            Properties.Settings.Default.Save();
        }

        protected virtual void DrawKeyboard(OverlayKeyboard kb)
        {
            // We store these to do highlighting (below)
            GridPosition currentSelectedKey = kb.CurrentGridPosition;
            GridPosition lastSelectedKey = kb.SelectedGridPosition ?? new GridPosition(0, 0);

            // Create rows and columns the size of our keyboard
            KeyboardGrid.Children.Clear();
            int i, rowVKeyPos, colVKeyPos;
            for(i = 0, KeyboardGrid.ColumnDefinitions.Clear(); i < kb.PGrid.Columns; i++, KeyboardGrid.ColumnDefinitions.Add(new ColumnDefinition())) { }
            for(i = 0, KeyboardGrid.RowDefinitions.Clear(); i < kb.PGrid.Rows; i++, KeyboardGrid.RowDefinitions.Add(new RowDefinition())) { }

            for(rowVKeyPos = 0; rowVKeyPos < kb.VGrid.VKeys.GetLength(0); rowVKeyPos++)
            {
                for(colVKeyPos = 0; colVKeyPos < kb.VGrid.VKeys.GetLength(1); colVKeyPos++)
                {
                    bool isSelected = (currentSelectedKey.Row == rowVKeyPos) && (currentSelectedKey.Column == colVKeyPos);
                    bool isLastSelected = (lastSelectedKey.Row == rowVKeyPos) && (lastSelectedKey.Column == colVKeyPos);
                    Viewbox tempViewbox = new Viewbox()
                    {
                        VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                    };
                    TextBlock tempKeyBlock = new TextBlock()
                    {
                        Text = kb.VGrid.VKeys[rowVKeyPos, colVKeyPos],
                        FontWeight = FontWeights.Bold,
                        Foreground = isSelected ? new SolidColorBrush(Colors.SteelBlue) : new SolidColorBrush(Colors.DarkGray),
                        Background = isLastSelected ? new RadialGradientBrush(Colors.Orange, Colors.White)  : new RadialGradientBrush(Colors.LightGray, Colors.White)                               
                    };
                    Grid.SetRow(tempViewbox, rowVKeyPos);
                    Grid.SetColumn(tempViewbox, colVKeyPos);
                    tempViewbox.Child = tempKeyBlock;
                    KeyboardGrid.Children.Add(tempViewbox);
                }
            }
        }

        private bool CheckRemoteReady()
        {
            if(_remoteControl == null) System.Windows.MessageBox.Show("Please load keyboard XML first");
            return _remoteControl != null;
        }

        private bool CheckVoiceCommandsReady()
        {
            if(String.IsNullOrEmpty(VoiceCommandsComboBox.Text)) System.Windows.MessageBox.Show("Please load voice commands first");
            return !String.IsNullOrEmpty(VoiceCommandsComboBox.Text);
        }

        private void CycleEdges_Checked(object sender, RoutedEventArgs e)
        {
            _kb.CycleEdges = CycleEdges.IsChecked.Value;
        }

        private void RemoteButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(CheckRemoteReady()) _remoteControl.DoOK(); // an "OK" from the remote is the same as a select
        }

        private void RemoteButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if(CheckRemoteReady()) _remoteControl.DoDownArrow();
        }

        private void RemoteButtonRight_Click(object sender, RoutedEventArgs e)
        {
            if(CheckRemoteReady()) _remoteControl.DoRightArrow();
        }

        private void RemoteButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            if(CheckRemoteReady()) _remoteControl.DoLeftArrow();
        }

        private void RemoteButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if(CheckRemoteReady()) _remoteControl.DoUpArrow();
        }

        private void RemoteButtonExecuteVoiceCommand_Click(object sender, RoutedEventArgs e)
        {
            if(CheckRemoteReady() && CheckVoiceCommandsReady())
            {
                PathOutput.Document = new FlowDocument();
                _kb.SpaceOccurred += OnKeyboardSpaceOccurred;
                PathOutput.AppendText(string.Format("{0}Path beginning at ({1},{2}) for \"{3}\"{0}{0}",
                     Environment.NewLine,
                     _kb.CurrentGridPosition.Row,
                     _kb.CurrentGridPosition.Column,
                     VoiceCommandsComboBox.Text));
                _remoteControl.DoVoiceToText(VoiceCommandsComboBox.Text);
                _kb.SpaceOccurred -= OnKeyboardSpaceOccurred;
                PathOutput.AppendText(Environment.NewLine);
            }
        }
    }
}
