layer collegato all'hardware:
non contiene logica ad esclusione di quello che serve per mappare le porte fisiche su un numero preciso di oggetti che espongono una delle due interfacce:

IInput
- digitalInput
- analogInput					max bits
- numericInput<Type>			int, float

IOutput
- digitalOuput
- analogOutput					max bits
- numericOutput<Type>			int, float

espone
- inputs (di tipo IInput)
	rx01, rx02, ra01, rn01<in>, rn02<float>, ecc.
- ouputs (di tipo IOuput)
	wx01, wx02, wa01, wn01<in>, wn02<float>, ecc.

----------
IInput.getValue()				per leggere lo stato
IOutput.setValue(Value)			per scrivere lo stato