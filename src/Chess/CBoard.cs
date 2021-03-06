﻿using Chess.Enums;
using Chess.Pieces;

namespace Chess
{
	public class CBoard
	{
		private readonly CPiece[] _pieces = new CPiece[64];
		
		public CPiece this[string coordinate]
		{
			get { return this[CSquare.Parse(coordinate)]; }
			set { this[CSquare.Parse(coordinate)] = value; }
		}

		public CPiece this[CSquare square]
		{
			get { return this[square.File, square.Rank]; }
			set { this[square.File, square.Rank] = value; }
		}

		public CPiece this[int file, int rank]
		{
			get { return _pieces[file + 8 * rank]; }
			set
			{
				_pieces[file + 8 * rank] = value;
			}
		}

		//public CPiece this[int file, int rank]
		//{
		//	get { return _pieces[file, rank]; }
		//	set
		//	{
		//		var oldValue = _pieces[file, rank];

		//		if (oldValue != value)
		//		{
		//			oldValue?.MoveTo(null, oldValue.File, oldValue.Rank);
		//			value?.MoveTo(this, file, rank);
		//		}
		//	}
		//}

		//internal void SetPiece(CPiece piece, int file, int rank)
		//{
		//	if (piece != null)
		//	{
		//		_pieces[file, rank]?.MoveTo(null, file, rank);
		//	}

		//	_pieces[file, rank] = piece;
		//}

		public static CBoard GetDefaultBoard()
		{
			return new CBoard
			{
				["A1"] = new CPieceRook(EPlayer.White),
				["B1"] = new CPieceKnight(EPlayer.White),
				["C1"] = new CPieceBishop(EPlayer.White),
				["D1"] = new CPieceQueen(EPlayer.White),
				["E1"] = new CPieceKing(EPlayer.White),
				["F1"] = new CPieceBishop(EPlayer.White),
				["G1"] = new CPieceKnight(EPlayer.White),
				["H1"] = new CPieceRook(EPlayer.White),

				["A2"] = new CPiecePawn(EPlayer.White),
				["B2"] = new CPiecePawn(EPlayer.White),
				["C2"] = new CPiecePawn(EPlayer.White),
				["D2"] = new CPiecePawn(EPlayer.White),
				["E2"] = new CPiecePawn(EPlayer.White),
				["F2"] = new CPiecePawn(EPlayer.White),
				["G2"] = new CPiecePawn(EPlayer.White),
				["H2"] = new CPiecePawn(EPlayer.White),

				["A8"] = new CPieceRook(EPlayer.Black),
				["B8"] = new CPieceKnight(EPlayer.Black),
				["C8"] = new CPieceBishop(EPlayer.Black),
				["D8"] = new CPieceQueen(EPlayer.Black),
				["E8"] = new CPieceKing(EPlayer.Black),
				["F8"] = new CPieceBishop(EPlayer.Black),
				["G8"] = new CPieceKnight(EPlayer.Black),
				["H8"] = new CPieceRook(EPlayer.Black),

				["A7"] = new CPiecePawn(EPlayer.Black),
				["B7"] = new CPiecePawn(EPlayer.Black),
				["C7"] = new CPiecePawn(EPlayer.Black),
				["D7"] = new CPiecePawn(EPlayer.Black),
				["E7"] = new CPiecePawn(EPlayer.Black),
				["F7"] = new CPiecePawn(EPlayer.Black),
				["G7"] = new CPiecePawn(EPlayer.Black),
				["H7"] = new CPiecePawn(EPlayer.Black),
			};

		}

		public bool IsCheck(EPlayer player)
		{
			return false;
		}

		public bool IsCheckMate(EPlayer player)
		{
			return false;
		}

		public bool WasCastling(EPlayer player)
		{
			return false;
		}
	}
}