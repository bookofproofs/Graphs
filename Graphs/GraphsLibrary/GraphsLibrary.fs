namespace GraphsLibrary
open System.Collections.Generic
open System

module GraphBasics =


    type IVertex =
        abstract member Value : obj with get, set

    type Vertex<'T when 'T :> obj> =
        { 
            mutable Value : 'T
        }
        interface IVertex with
            member this.Value
                with get() = box this.Value
                and set(value) = this.Value <- unbox<'T> value 

    type Arrow = 
        {
            Start: IVertex
            End: IVertex
        }

    type DiGraph() =
        let vertices = HashSet<IVertex>()
        let arrows = HashSet<Arrow>()
        
        /// Adds a vertex to the directed graph.
        member this.AddVertex(vertex: IVertex) =
            vertices.Add(vertex)

        /// Removes a vertex from the directed graph, also removing the related arrows.
        member this.RemoveVertex(vertex: IVertex) = 
            let toBeDeletedEdges = 
                arrows
                |> Seq.filter (fun e -> e.Start = vertex || e.End = vertex) 
            toBeDeletedEdges
            |> Seq.iter (fun e -> arrows.Remove(e) |> ignore)
            vertices.Remove(vertex)

        /// Returns the set of vertices of the directed graph.
        member this.Vertices = vertices

        /// Adds an arrow to the graph. If the starting and ending
        /// vertices of the arrow are not in the set of vertices of the graph,
        /// they will be added to it.
        member this.AddArrow(arrow: Arrow) = 
            if not (vertices.Contains(arrow.Start)) then
                vertices.Add(arrow.Start) |> ignore
            if not (vertices.Contains(arrow.End)) then
                vertices.Add(arrow.End) |> ignore
            arrows.Add(arrow)

        /// Removes an arrow from the directed graph
        member this.RemoveArrow(arrow: Arrow) = 
            arrows.Remove(arrow)

        /// Returns the set of edges of the directed graph.
        member this.Arrows = arrows

        /// Returns a string representation of the directed graph
        member this.ToString =
            let verticesWithArrows = HashSet<IVertex>()
            let stringVerticesWithArrows = 
                arrows
                |> Seq.map (fun a -> 
                    verticesWithArrows.Add(a.Start) |> ignore
                    verticesWithArrows.Add(a.End) |> ignore
                    sprintf "%A -> %A" a.Start.Value a.End.Value)
                |> String.concat ":"
            let stringVerticesWithoutArrows = 
                vertices
                |> Seq.filter (fun v -> not (verticesWithArrows.Contains(v)))
                |> Seq.map (fun v -> sprintf "%A" v.Value)
                |> String.concat ":"
            if stringVerticesWithoutArrows = "" && stringVerticesWithArrows = "" then
                "<empty>"
            elif stringVerticesWithoutArrows = "" && stringVerticesWithArrows <> "" then
                stringVerticesWithArrows
            elif stringVerticesWithoutArrows <> "" && stringVerticesWithArrows = "" then
                stringVerticesWithoutArrows
            else 
                stringVerticesWithoutArrows + ":" + stringVerticesWithArrows
            
            
   



