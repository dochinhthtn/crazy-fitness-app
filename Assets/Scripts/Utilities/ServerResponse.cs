[System.Serializable]
public class ServerResponseLink {
    public string first;
    public string last;
    public string prev;
    public string next;
}
[System.Serializable]
public class ServerResponse<T> {
    public T data;
    public ServerResponseLink links;
}