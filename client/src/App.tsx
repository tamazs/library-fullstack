import { useState } from 'react'
import {finalUrl} from "./baseUrl.ts";

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
        <button className="btn btn-primary" onClick={() => {
            fetch(finalUrl + "GetAllGenres")
                .then(res => {
                    console.log(res)
                }).catch(err => console.log(err))
        }}>Click</button>
    </>
  )
}

export default App
