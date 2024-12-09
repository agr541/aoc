module D202401

open System
open System.Collections.Generic
open System.IO


let A (inputFile: string) =
    let input = File.ReadAllText(inputFile)
    let parseLines (input: string) = input.Split("\n")

    let parseLine (line: string, index: int) =
        line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        |> Array.item (index)
        |> Int32.Parse

    let parsedLines = input |> parseLines
    let colA = parsedLines |> Array.map (fun line -> parseLine (line, 0)) |> Array.sort
    let colB = parsedLines |> Array.map (fun line -> parseLine (line, 1)) |> Array.sort
    let absDiffs = Array.map2 (fun valA valB -> abs (valA - valB)) colA colB
    let answer = Array.sum absDiffs
    answer

let B (inputFile: string) =
    let input = File.ReadAllText(inputFile)
    let parseLines (input: string) = input.Split("\n")

    let parseLine (line: string, index: int) =
        line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        |> Array.item (index)
        |> Int32.Parse

    let parsedLines = input |> parseLines
    let colA = parsedLines |> Array.map (fun line -> parseLine (line, 0)) |> Array.sort
    let colB = parsedLines |> Array.map (fun line -> parseLine (line, 1)) |> Array.sort

    let scores =
        Array.map (fun valA -> (valA * (colB |> Array.filter (fun valB -> valB = valA) |> Array.length))) colA

    let answer = Array.sum scores
    answer

let inputs = Directory.EnumerateFiles(__SOURCE_DIRECTORY__, "*.txt")

let runOnInputs (func: (string -> int), inputs: IEnumerable<string>) =
    inputs
    |> Seq.map (fun input ->
        let result = func input
        $"{Path.GetFileNameWithoutExtension(input)}:{result}")

let methods =
    Map.ofList
        [ ("1A", (fun () -> runOnInputs (A, inputs)))
          ("1B", (fun () -> runOnInputs (B, inputs))) ]
    |> Map.toList
