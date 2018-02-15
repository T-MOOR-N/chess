﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Chess;
using Chess.Enums;
using Chess.Moves;
using Chess.Pieces;

namespace ChessApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private CGame _game;
		private EPlayer _player = EPlayer.White;
		private readonly Random _random = new Random();

		public MainWindow()
		{
			InitializeComponent();
			
			Reset();
		}

		private void Reset()
		{
			ChessBoard.Board =   CBoard.GetDefaultBoard();

			//_board = new CBoard
			//{
			//	["B7"] = new CPieceKing(EPlayer.White),
			//	["D7"] = new CPieceKing(EPlayer.Black),
			//	["E5"] = new CPieceQueen(EPlayer.White),
			//	["E6"] = new CPieceKnight(EPlayer.White),
			//	["E7"] = new CPieceBishop(EPlayer.Black),
			//};

			//_board = new CBoard
			//{
			//	["H6"] = new CPieceKing(EPlayer.Black),
			//	["H8"] = new CPieceKing(EPlayer.White),
			//	["G1"] = new CPieceQueen(EPlayer.Black)
			//};

			_game = new CGame(ChessBoard.Board);
			_player = EPlayer.White;
			ChessBoard.InvalidateVisual();
			ListBox.ItemsSource = null;
		}

		private void Do(CMove move)
		{
			_game.Do(move);
			Update();
		}

		private void Undo()
		{
			if (_game.Undo())
			{
				Update();
			}
		}

		private void Redo()
		{
			if (_game.Redo())
			{
				Update();
			}
		}

		private void Update()
		{
			_player = 1 - _player;
			ChessBoard.InvalidateVisual();

			var history = _game.GetHistory();

			var result = new List<string>();
			for (var i = 0; i < history.Count; i += 2)
			{
				var num = i/2 + 1;
				var numSize = num.ToString().Length;
				var spaces = new string(' ', Math.Max(2, numSize) - numSize);

				var text = $"{spaces}{num}. ";
				text += $"{history[i]}";
				if (i + 1 < history.Count)
				{
					text += $"\u2003{history[i + 1]}";
				}
				result.Add(text);
			}

			ListBox.ItemsSource = result;
			ListBox.ScrollIntoView(result.LastOrDefault());
		}

		private void DoButton_Click(object sender, RoutedEventArgs e)
		{
			//Выбираем все допустимые ходы
			var moves = _game.GetAllMoves(_player).Where(x =>
			{
				x.Do();
				var result = _game.GetAllMoves(1 - _player) != null;
				x.Undo();
				return result;
			}).ToList();
			
			if (moves.Count == 0)
			{
				MessageBox.Show("Некуда ходить!");
			}
			else
			{
				var index = _random.Next(0, moves.Count);
				Do(moves[index]);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			var isControlKey = e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl);

			switch (e.Key)
			{
				case Key.Back:
					Undo();
					break;

				case Key.Z:
					if (isControlKey)
					{
						Undo();
					}
					break;


				case Key.Y:
					if (isControlKey)
					{
						Redo();
					}
					break;
			}
		}

		private void UndoButton_Click(object sender, RoutedEventArgs e)
		{
			Undo();
		}

		private void RedoButton_Click(object sender, RoutedEventArgs e)
		{
			Redo();
		}
		
		private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
		{
			var depth = 2;
			double tick = DateTime.Now.Ticks;
			int result;
			_game.Analyze(EPlayer.White, depth, out result);
			tick = (DateTime.Now.Ticks - tick)/10000D;
			MessageBox.Show($"Глубина анализа ходов: {depth}.\r\nПроанализировано варинантов: {result}.\r\nВремя выполнения: {tick} мс.");
		}
		
		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			Reset();
		}
	}
}
