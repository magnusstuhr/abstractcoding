# AbstractCoding
Example code trying to reflect how code that "works" is not good enough. Writing clean code with clean architecture ensures that the code is much easier to read, extend and maintain in the long run.

The code is especially trying to follow the following software engineering principles to the fullest:
* "Open-closed" (https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle)
* "Stable abstractions" (https://en.wikipedia.org/wiki/Package_principles)
* "Dependency Inversion" (https://en.wikipedia.org/wiki/Dependency_inversion_principle)
* "Don't Repeat Yourself" (https://en.wikipedia.org/wiki/Don%27t_repeat_yourself)

## Illustration of code-evolvement
See the different tags / releases to see how the code has evolved to be more abstract, loosely coupled, and have duplication removed.

### 0.1.0
The HttpRequestOperator class fullfil its requirements, but there are both structural and semantic code-duplication in every method.

#### 0.1.1
The HttpRequestOperator class' structural code-duplication in the HttpRequestOperator class is removed, but the semantic code-duplication remains.

### 0.2.0
The HttpRequestOperator class' structural and semantic code-duplication are now eliminated by using Funcs and local methods to parameterize the isolated shared functionality.

### 1.0.0
The HttpRequestOperator class now relies on the strongly typed abstract class "HttpRequest" with the abstract method "HttpRequest.Execute" instead of local methods and Funcs.

### 2.0.0
The HttpRequestOperator class now relies on an injectedI IHttpRequestFactory interface for creating the different types of IHttpRequest, to eliminate the dependencies to the concrete implementations, thus following the Dependency Inversion Principle to the fullest.
