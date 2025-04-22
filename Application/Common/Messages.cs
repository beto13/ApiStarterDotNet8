namespace Application.Common
{
    public static class Messages
    {
        public static class Post
        {
            public const string Created = "Post creado correctamente.";
            public const string Updated = "Post actualizado correctamente.";
            public const string Deleted = "Post eliminado correctamente.";
            public const string NotFound = "No se encontró el post solicitado.";
            public const string CreateError = "Error al crear post.";
            public const string UpdateError = "Error al actualizar post.";
            public const string DeleteError = "Error al eliminar post.";
        }

        public static class User
        {
            public const string Created = "Usuario creado correctamente.";
            public const string Updated = "Usuario actualizado correctamente.";
            public const string Deleted = "Usuario eliminado correctamente.";
            public const string NotFound = "No se encontró el Usuario solicitado.";
            public const string CreateError = "Error al crear Usuario.";
            public const string UpdateError = "Error al actualizar Usuario.";
            public const string DeleteError = "Error al eliminar Usuario.";
            public const string EmailAlreadyExists = "El email ingresado ya existe.";
        }

        public static class Comment
        {
            public const string Created = "Comment creado correctamente.";
            public const string Updated = "Comment actualizado correctamente.";
            public const string Deleted = "Comment eliminado correctamente.";
            public const string NotFound = "No se encontró el comment solicitado.";
            public const string CreateError = "Error al crear comment.";
            public const string UpdateError = "Error al actualizar comment.";
            public const string DeleteError = "Error al eliminar comment.";
        }
    }
}
