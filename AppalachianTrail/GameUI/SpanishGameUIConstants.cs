namespace GameUI
{
    public class SpanishGameUIConstants : GameUIConstants
    {
        public override string Title => @"
            
  _______ _                                       _            _     _               _______        _ _ 
 |__   __| |              /\                     | |          | |   (_)             |__   __|      (_) |
    | |  | |__   ___     /  \   _ __  _ __   __ _| | __ _  ___| |__  _  __ _ _ __      | |_ __ __ _ _| |
    | |  | '_ \ / _ \   / /\ \ | '_ \| '_ \ / _` | |/ _` |/ __| '_ \| |/ _` | '_ \     | | '__/ _` | | |
    | |  | | | |  __/  / ____ \| |_) | |_) | (_| | | (_| | (__| | | | | (_| | | | |    | | | | (_| | | |
    |_|  |_| |_|\___| /_/    \_\ .__/| .__/ \__,_|_|\__,_|\___|_| |_|_|\__,_|_| |_|    |_|_|  \__,_|_|_|
                               | |   | |                                                                
                               |_|   |_|                                                                
";

        public override string ConsoleTitle => "The Appalachian Trail";
        public override string ShopItems => "1. Botella de agua \t$3 \n" +
                                        "\t2. Conjuntos de Ropa \t$10 \n" +
                                        "\t3. La Carpa \t \t$200 \n" +
                                        "\t4. Onzas de comida \t$1 \n" +
                                        "\t5. El Saco de Dormir \t$100 \n";

        public override string HelloTraveler => "\nHola Viajero/a";
        public override string Welcome => "Y Bienvenidos a ...";
        public override string Intro => "Estás a punto de embarcarte en un viaje de 2,190 millas que se extiende desde Georgia hasta Maine";
        public override string Start => "Tienes un largo camino por delante, así que empecemos";
        public override string PlayerName => "Cuál es tu Nombre? ";
        public override string Occupation => "¿Cuál es su ocupación? ? [Doctor, Carpintero, Estudiante, Hippie]: ";
        public override string NotVaild => " no es una respuesta válida, por favor intente de nuevo...";
        public override string UserStartPoint => "¿Cuándo te gustaría comenzar tu caminata en el AT? \n\t\t(Por favor, especifique el mes completo o el número del mes ej.Marzo o 3): ";
        public override string NotVaildStartDate => " no es una respuesta válida, por favor intente de nuevo...";
        public override string Date => "\n\tFecha: ";
        public override string Weather => "\tClima: ";
        public override string Health => "\tSalud: ";
        public override string Food => "\tComida: ";
        public override string Landmark => "\tSiguiente hito: ";
        public override string Miles => "\tmillas recorridas: ";
        public override string ContinueTrail => "Introduzca 1 para continuar en el camino.";
        public override string Rest => "Introduce 2 para descansar";
        public override string ChangePace => "Introduce 3 para cambiar el ritmo";
        public override string ChangeFoodRatio => "Introduce 4 para cambiar las raciones de comida.";
        public override string Choice => "tu elección: ";
        public override string PurchaseSupplies => "Introduce 1 para comprar algunos suministros.";
        public override string ContinueTrailhead => "Introduzca 3 para continuar hasta el próximo comienzo del sendero.";
        public override string SpeakTownsfolk => "Introduzca 4 para hablar gentes del pueblo.";
        public override string EndGame => "Felicidades viajero/a! Lo lograste a llegar Rangeley, ME! ¿Te gustaría grabar tu puntuación más alta? ? ";
        public override string RecordHighScore => " ¿Te gustaría grabar tu puntuación más alta? ";
        public override string EnterHighScore => "Introduzca 1 para grabar su puntuación más alta";
        public override string Quit => "Introduce 2 para salir";
        public override string WelcomeSupplyStore => "Bienvenido a la tienda de suministros. ¿Qué te gustaría comprar?";
        public override string SupplyStoreItems => "tenemos los siguientes artículos en stock.";
        public override string Currently => "Tu Actualmente tienes ";
        public override string Spend => " para gastar.";
        public override string PurchaseQuestion => "¿Te gustaría comprar algo?? (S/N): ";
        public override string CurrentCart => "\n\tSus artículos actuales en el carrito de compras ";
        public override string CurrentBackpack => "\n\tSus artículos actuales en la mochila";
        public override string SelectItem => "Seleccione un artículo para comprar [1-5]: ";
        public override string PurchaseSize => "¿Cuántos te gustaría comprar?: ";
        public override string InsufficientFunds => "No tienes suficiente dinero para eso.";
        public override string PaceQuestion => "¿Qué ritmo te gustaría caminar?";
        public override string PaceOptions => "[1] Rapido / [2] Estable / [3] Lento";
        public override string CurrentPace => "Tu ritmo actual: ";
        public override string FoodRationQuestion => "¿Cuántas raciones de alimentos te gustaría consumir??";
        public override string FoodRationOption => "[1] Relleno / [2] pobre / [3] puro huesos";
        public override string CurrentFoodRation => "Tus raciones alimenticias actuales.: ";


    }
}
