module puzzle7

open System
open System.Collections.Generic



let splitLineToArray (line: string) (splitChar: char)=
    let lineArray = line.Split([|splitChar|], System.StringSplitOptions.RemoveEmptyEntries) |> Array.map System.Int32.Parse
    lineArray

let convertListToArray (data: string list) =
    let listOfArrays = data |> List.map (fun x -> splitLineToArray x ' ')
    listOfArrays

let transposeTheList (data: int[] list) =
    let transposed = data |> List.toArray |> Array.transpose |> Array.toSeq |> Seq.toList
    let all = data  @ transposed
    all

let makeBoards (data: string list) =
    let noBlankLinesData = data |> List.filter (fun (x: string) -> x.Length > 0)
    //printfn "There are %d lines in the new list" noBlankLinesData.Length
    let listOfLists = noBlankLinesData |> List.chunkBySize 5 
    //printfn "There are %d lists" listOfLists.Length
    let listOfArrays = listOfLists |> List.map (fun x -> convertListToArray x)
    let listOfCards = listOfArrays |> List.map transposeTheList 
    listOfCards


let removeIfContains 
    (theArray: int[])
    (theNumber: int) =
    let returnArray = theArray |> Array.filter (fun x -> x <> theNumber)
    returnArray

let rec callNumbersAgainstCard
    (theCard: int[] list)
    (theCallNumbers: int[])
    (theIndex: int) =
    let updatedCard = theCard |> List.map (fun x -> removeIfContains x theCallNumbers.[theIndex])
    let cardIsWinner = updatedCard |> List.exists (fun x -> x.Length = 0)
    let exhaustedNumbers = (theIndex + 1 >= theCallNumbers.Length)
    if cardIsWinner = true then  
        let sumOfUncalled = updatedCard |> List.map (fun x -> x |> Array.sum) |> List.sum
        let halfSum = sumOfUncalled / 2
        let product = theCallNumbers.[theIndex] * halfSum
        (theIndex, product)
    elif exhaustedNumbers = true then
        // monkey balls initial "didn't win" solution - steps = 1000 and zero score
        // there is a better way
        (1000,0)
    else
        let nextIndex = theIndex + 1
        callNumbersAgainstCard updatedCard theCallNumbers nextIndex
        
let playTheGame
    (theCallNumbers: int[])
    (theBingoCards: int[] list list) =
    let result = theBingoCards |> List.map (fun x -> callNumbersAgainstCard x theCallNumbers 0)
    let winningCard = result |> List.sortBy (fun x -> fst(x)) |> List.head
    let steps = fst(winningCard) + 1
    let score = snd(winningCard)
    (steps, score)

let composeCallListAndBoards (data: string) =
    let splitLines = List.ofSeq(data.Split([|'\n'|]))
    let callArray = splitLineToArray splitLines.[0] ','
    let boardData = splitLines |> List.skip 2
    let boards = makeBoards boardData
    (callArray, boards)

let puz7 =
    let data = IO.File.ReadAllText @"C:\adventofcode\04\input.txt"
    let (callNumbers, bingoCards) = composeCallListAndBoards data
    let (numberBalls, score) = playTheGame callNumbers bingoCards
    printfn "Puzzle 7:  Winning card took %d balls and scored %d" numberBalls score
    0


