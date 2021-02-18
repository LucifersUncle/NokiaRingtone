// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open FParsec

type Token = {
    Duration : int32
    Key : string
    Octave: int32
}

let initialChord = "8#d2"



let AddDuration (dur: int32 ) (stream:CharStream<Token>) =
    let s = stream.UserState
    stream.UserState <- {s with Duration = dur}
    Reply dur
    
let AddKey (key: string ) (stream:CharStream<Token>) =
    let s = stream.UserState
    stream.UserState <- {s with Key = key}
    Reply key
        
let AddOctave (octave: int32 ) (stream:CharStream<Token>) =
    let s = stream.UserState
    stream.UserState <- {s with Octave = octave}
    Reply octave
        
let duration: Parser<int32, Token> =
    pint32 .>> anyChar >>= AddDuration
    
let key: Parser<string, Token> =
    manyChars (notFollowedBy digit >>. anyChar) >>= AddKey

let Octave: Parser<int32, Token> =
    pint32 >>= AddOctave

let part1 =  digit

let defaultState = {
    Duration = 0
    Key = "0"
    Octave = 0
}

[<EntryPoint>]
let main argv =

    match runParserOnString Octave defaultState "" "2asdasda" with
    | Success(result, _, _) ->  printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg
    
    0 // return an integer exit code