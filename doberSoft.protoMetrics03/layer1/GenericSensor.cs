using doberSoft.protoMetrics03.layer0;
using doberSoft.protoMetrics03.Rules;
using doberSoft.protoMetrics03.ScaleFunctions;

namespace doberSoft.protoMetrics03.layer1
{
    public class GenericSensor<Tin, Tout> : AbstractSensor<Tin, Tout>
    {
        public GenericSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            IRules rules,
            IInput<Tin> input)
                : base(name, id, scaleFunction, rules)
        {
            _inputs = new IInput<Tin>[1];
            _inputs[0] = input;
        }

        public GenericSensor(
            string name,
            int id,
            IScale<Tin, Tout> scaleFunction,
            IRules rules,
            IInput<Tin> input1,
            IInput<Tin> input2)
                : base(name, id, scaleFunction, rules)
        {
            _inputs = new IInput<Tin>[2];
            _inputs[0] = input1;
            _inputs[1] = input2;
        }
    }


}
