import { useEffect, useState } from 'react'
import axios from 'axios';

export function Movies() {
    const [movies, setMovies] = useState();
    const [name, setName] = useState("");
    const [desc, setDesc] = useState("");
    const [date, setDate] = useState("");
    const [file, setFile] = useState();

    useEffect(() => {
        fetch('http://localhost:60060/movies')
            .then((res) => res.json())
            .then((data) => setMovies(data))
    }, [])
    const saveFile = (e) => {
        setFile(e.target.files[0])
        console.log(date);
    }
    const submitForm = async (e) => {
        e.preventDefault();
        const formData = new FormData()
        formData.append("Name", name)
        formData.append("Description", desc)
        formData.append("RealeaseDate", date)
        formData.append("Image", file)
        console.log(file)

        const res = await axios.post('http://localhost:60060/movies/add', formData)
        console.log(res);
    }
    return (
        <>
            <h1>Our movies</h1>
            <form action="#" method="post" encType="multipart/form-data">
                <label htmlFor="Name">Movie title</label>
                <input name="Name" value={name} onChange={(e) => setName(e.target.value)} type="text" />
                <label htmlFor="Description">Description</label>
                <textarea value={desc} onChange={(e) => setDesc(e.target.value)}></textarea>
                <label htmlFor="RealeaseDate">Realease date</label>
                <input name="RealeaseDate" type="date" value={date} onChange={(e) => setDate(e.target.value)} />
                <label htmlFor="Image">Image</label>
                <input type="file" onChange={(e) => saveFile(e)} />
                <input type="button" className="btn btn-primary" value="Add movie" onClick={(e) => submitForm(e)} />
            </form>
            {movies !== undefined ? movies.map((movie, key) => {
                return (
                    <div className="movie-card" style={{backgroundImage: "url(" + movie.ImagePath + ")"}} key={key}>
                        <h3>{movie.Name}</h3>
                        <p>{movie.Description}</p>
                        <p>Realeased at: <b>{movie.RealeaseDate}</b></p>
                    </div>
                )
            }) : <p>Loading....</p>}
        </>
    )
}