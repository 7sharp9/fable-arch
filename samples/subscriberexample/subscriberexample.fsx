(**
 - title: Subscriber example - demo of how to subscribe to events from the application
 - tagline: Subscribing on events from the fable-virtualdom application
 - app-style: width:800px; margin:20px auto 50px auto;
 - intro: Simple hello world application, but with logging. Open console to see.
*)

#r "node_modules/fable-core/Fable.Core.dll"
#load "node_modules/fable-import-virtualdom/Fable.Helpers.Virtualdom.fs"

open Fable.Core
open Fable.Core.JsInterop

open Fable.Helpers.Virtualdom
open Fable.Helpers.Virtualdom.App
open Fable.Helpers.Virtualdom.Html

// Model
type Model = string

type TodoAction =
    | ChangeInput of string

// Update
let update model msg =
    match msg with
    | ChangeInput str -> str

// View
let inline onInput x = onEvent "oninput" (fun e -> x (unbox e?target?value)) 
let view model =
    div
        []
        [
            label 
                []
                [text "Enter name: "]
            input
                [
                    onInput (fun x -> ChangeInput x)
                ]
            br []
            span
                []
                [text (sprintf "Hello %s" model)]
        ]

// Using createSimpleApp instead of createApp since our
// update function doesn't generate any actions. See 
// some of the other more advanced examples for how to
// use createApp
createSimpleApp "" view update
|> withStartNodeSelector "#hello"
|> withSubscriber "Logger" (fun x -> Fable.Import.Browser.console.log("Event received: ", x))
|> start renderer
