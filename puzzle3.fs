module puzzle3

open System


// makeMovementTuple takes the given string, splits it on the space between the direction and the distance, converts the distance to an integer and returns a tuple
let makeMovementTuple (text : string) =
  let trimmed = text.Trim()
  let parts = trimmed.Split [|' '|] 
  let direction = parts.[0]
  let distance = parts.[1] |> int
  (direction, distance)


let puz3 =
    let lines = IO.File.ReadLines @"C:\adventofcode\02\input.txt"
    // turn the lines of strings into a sequence of tuples
    let values = lines |> Seq.map makeMovementTuple
    // filter the directions, grab all the distances and sum them
    let ups = values |> Seq.filter (fun (x, y) -> x.Equals("up")) |> Seq.map (fun (x,y) -> y) |> Seq.sum
    let downs = values |> Seq.filter (fun (x, y) -> x.Equals("down")) |> Seq.map (fun (x,y) -> y) |> Seq.sum
    // the vertical change is the amount down minus the amount up, since this is under water
    let vertical = downs - ups
    let forwards = values |> Seq.filter (fun (x, y) -> x.Equals("forward")) |> Seq.map (fun (x,y) -> y) |> Seq.sum
    // compute the vertical and horizontal product
    let product = vertical * forwards
    printfn "Puzzle 3:  The distance is %d" product