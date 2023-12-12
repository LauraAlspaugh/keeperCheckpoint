export class Profile {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.picture = data.picture
        this.coverImg = data.coverImg || "https://images.unsplash.com/photo-1536009190979-329b94ad4d57?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8YWVzdGhldGljJTIwYmFja2dyb3VuZHxlbnwwfHwwfHx8MA%3D%3D"
    }
}