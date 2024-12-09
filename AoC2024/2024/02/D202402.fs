module D202402

open System
open System.Collections.Generic
open System.IO


let isSafe (values: int[]) =
    let diffs = values |> Seq.pairwise |> Seq.map (fun (a, b) -> (a - b))
    let min = diffs |> Seq.min
    let max = diffs |> Seq.max

    let safe =
        ((min < 0 && max < 0) || (min > 0 && max > 0))
        && diffs |> Seq.map abs |> Seq.forall (fun a -> a > 0 && a < 4)

    safe

let isSafeB (values: int[]) =

    let mutable safe = isSafe (values)

    if safe = false then
        let mutable i = 0

        while i < values.Length do
            let newValues = values |> Seq.removeAt (i) |> Array.ofSeq
            let isSafeWithValueRemoved = isSafe newValues

            if isSafeWithValueRemoved then
                i <- values.Length
                safe <- isSafeWithValueRemoved
            else
                i <- i + 1

    safe

let A (inputFile: string) =
    let input = File.ReadAllText(inputFile)
    let parseLines (input: string) = input.Split("\n")

    let parseLine (line: string) =
        line.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map Int32.Parse

    let parsedLines = input |> parseLines |> Array.map parseLine

    let lineResults =
        parsedLines |> Array.map (fun a -> $"{String.Join(' ', a)}:{isSafe (a)}")

    let answer = parsedLines |> Array.filter isSafe |> Array.length
    printfn $"{String.Join('\n', lineResults)}\n{answer}"
    answer


let B (inputFile: string) =
    let input = File.ReadAllText(inputFile)
    let parseLines (input: string) = input.Split("\n")

    let parseLine (line: string) =
        line.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map Int32.Parse

    let parsedLines = input |> parseLines |> Array.map parseLine

    let lineResults =
        parsedLines |> Array.map (fun a -> $"{String.Join(' ', a)}:{isSafeB (a)}")

    let answer = parsedLines |> Array.filter isSafeB |> Array.length
    printfn $"{String.Join('\n', lineResults)}\n{answer}"
    answer

let inputs = Directory.EnumerateFiles(__SOURCE_DIRECTORY__, "*.txt")

let runOnInputs (func: (string -> int), inputs: IEnumerable<string>) =
    inputs
    |> Seq.map (fun input ->
        let result = func input
        $"{Path.GetFileNameWithoutExtension(input)}:{result}")

let methods =
    Map.ofList
        [ ("2A", (fun () -> runOnInputs (A, inputs)))
          ("2B", (fun () -> runOnInputs (B, inputs))) ]
    |> Map.toList
