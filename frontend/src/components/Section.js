const Section = (props) =>{
  return (
    <section id={props.id} className={"col-"+props.colSize+" bg-light bg-gradient p-3 pt-5 card"}>
      {props.children}
    </section>
  )
}

export default Section;