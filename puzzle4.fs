module puzzle4

open System

let makeMovementTuple (text : string) =
  let trimmed = text.Trim()
  let parts = trimmed.Split [|' '|] 
  let direction = parts.[0]
  let distance = parts.[1] |> int
  (direction, distance)

type PositionResult = 
    {
        CurrentAim : int
        VerticalPosition : int
        HorizontalPosition : int
    }

let initialState = 
    {
        CurrentAim = 0
        VerticalPosition = 0
        HorizontalPosition = 0
    }

let folder state (direction, distance) =
    match direction with 
    | "up" ->
        { state with CurrentAim = state.CurrentAim + -1 * distance }
    | "down" ->
        { state with CurrentAim = state.CurrentAim + distance }
    | "forward" ->
        { state with 
            HorizontalPosition = state.HorizontalPosition + distance
            VerticalPosition = state.VerticalPosition + distance * state.CurrentAim }

let puz4 =
    let lines = IO.File.ReadLines @"C:\adventofcode\02\input.txt"
    let values = lines |> Seq.map makeMovementTuple
    let answer = Seq.fold folder initialState values
    let product = answer.HorizontalPosition * answer.VerticalPosition
    printfn "Puzzle 4:  The distance is %d" product